using MauiBlazorWeb.Shared.Bases;

namespace MauiBlazorWeb.Shared.Interfaces
{
    public interface IDiaryCompSubject
    {
        void Attach(DiaryCompBase diaryComp);
        void Detach(DiaryCompBase diaryComp);
        Task UpdateDiaryComponents();
    }
}