-- -----------------------------------------------------
-- Schema diary_db
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `diary_db` DEFAULT CHARACTER SET utf8 ;
CREATE DATABASE IF NOT EXISTS `diary_db`
	DEFAULT CHARACTER SET utf8
	DEFAULT COLLATE utf8_general_ci;
USE `diary_db` ;
-- -----------------------------------------------------
-- Table `Account`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS Account (
  `id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `username` VARCHAR(100),
  `email` VARCHAR(100),
  `password_hash` BLOB,
  `password_salt` BLOB,
  `birthdate` DATE,
  `registrationdate` DATE,
  `gender` ENUM('Male', 'Female', 'Other'),
  admin BOOLEAN DEFAULT FALSE
);

-- -----------------------------------------------------
-- Table `Diary_log_column`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS Diary_log_column (
  `id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `name` VARCHAR(100),
  `type` ENUM('Habit', 'Words', 'NumberRange', 'Other'),
  `value_range_min` INT,
  `value_range_max` INT,
  `Account_id` INT NOT NULL,
  FOREIGN KEY (Account_id) REFERENCES Account(id)
  );

-- -----------------------------------------------------
-- Table `Diary_log_post`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS Diary_log_post (
  `id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `Diary_log_column_id` INT NOT NULL,
  `value` VARCHAR(1000),
  `date` DATE,
  FOREIGN KEY (Diary_log_column_id) REFERENCES Diary_log_column(id)
);

-- -----------------------------------------------------
-- Table `Sport`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS Sport (
  `id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `name` VARCHAR(100),
  `status` ENUM('Public', 'Private', 'Deleted'),
  `Creator_Account_id` INT NULL,
  FOREIGN KEY (Creator_Account_id) REFERENCES Account(id)
);

-- -----------------------------------------------------
-- Table `Account_does_Sport`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS Account_does_Sport (
  `id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `Account_id` INT NOT NULL,
  `Sport_id` INT NOT NULL,
  FOREIGN KEY (Account_id) REFERENCES Account(id),
  FOREIGN KEY (Sport_id) REFERENCES Sport(id)
);

-- -----------------------------------------------------
-- Table `Routine`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS Routine (
  `id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `status` ENUM('Public', 'Private', 'Deleted'),
  `Account_does_Sport_id` INT NOT NULL,
  `ExampleWorkout_id` INT NOT NULL,
  FOREIGN KEY (ExampleWorkout_id) REFERENCES Workout(id),
  FOREIGN KEY (Account_does_Sport_id) REFERENCES Account_does_Sport(id)
  );


-- -----------------------------------------------------
-- Table `Workout`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS Workout (
  `id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `name` VARCHAR(100),
  `isDone` TINYINT,
  `starttime` DATETIME,
  `duration` TIME,
  `notes` VARCHAR(1000),
  `isRoutineExample` TINYINT,
  `Routine_id` INT NULL,
  `Account_does_Sport_id` INT NOT NULL,
  FOREIGN KEY (Account_does_Sport_id) REFERENCES Account_does_Sport(id),
  FOREIGN KEY (Routine_id) REFERENCES Routine(id)
  );


-- -----------------------------------------------------
-- Table `Exercise`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS Exercise (
  `id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `name` VARCHAR(100),
  `notes` VARCHAR(1000),
  `rest` TIME,
  `type` ENUM('Bodyweight', 'Machine', 'Timed', 'Distanced', 'Other'),
  `status` ENUM('Public', 'Private', 'Deleted'),
  `Creator_Account_id` INT NULL,
  `Sport_id` INT NOT NULL,
  FOREIGN KEY (Creator_Account_id) REFERENCES Account(id),
  FOREIGN KEY (Sport_id) REFERENCES Sport(id)
);

-- -----------------------------------------------------
-- Table `Sets`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS Sets (
  `id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `workoutindex` INT,
  `isDone` TINYINT,
  `type` ENUM('Normal', 'Warmup', 'Drop') NOT NULL,
  `reps` INT,
  `RPE` INT,
  `weight` DOUBLE,
  `length` TIME,
  `distance` DOUBLE,
  `Exercise_id` INT NOT NULL,
  `Workout_id` INT NOT NULL,
  FOREIGN KEY (Exercise_id) REFERENCES Exercise(id),
  FOREIGN KEY (Workout_id) REFERENCES Workout(id)
  );
  
  insert into account(username, password_hash, password_salt, registrationdate, admin) values("admin",UNHEX("3F677C7B4969E38E416CD75FDC42FC062D238B96433798EDDABE23F056B47E8A"),UNHEX("0F86FB2E95E6120522AACAF551086D40"), '2023-09-10', true); -- Default values for the admin admin1 login