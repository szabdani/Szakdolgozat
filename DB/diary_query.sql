-- DROP DATABASE IF EXISTS diary_db;
USE `diary_db`;

select * from account;
select * from account where 'id' = 1;

-- update account set username = 'faaa' where id = 5;

select * from Diary_log_column;
select * from Diary_log_post;

-- Insert into Diary_log_column (name, type, account_id) values ('afff', 'Habit', 50);

select * from Diary_log_column where type != 'Habit';
select * from Diary_log_column;

select * from Diary_log_post where Diary_log_column_id = @colid order by date;
select * from Diary_log_post;

-- update account set registrationdate = '2024-09-10' where id = 2;

-- Select All --
select * from account;
select * from Diary_log_column;
select * from Diary_log_post;
select * from account_does_sport;
select * from sport;
select * from exercise;
select * from routine;
select * from workout;
select * from sets;

-- select * from sets; 
-- alter table routine add FOREIGN KEY (ExampleWorkout_id) REFERENCES Workout(id);
-- alter table exercise drop column `rest`;
-- alter table routine add FOREIGN KEY (Account_does_Sport_id) REFERENCES Account_does_Sport(id);