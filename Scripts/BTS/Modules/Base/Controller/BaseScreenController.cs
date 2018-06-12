using System;

public abstract class BaseScreenController<View>: BaseController<View>, IScreenController where View: IControlledView
{
    [Inject]
    protected IHistoryService m_historyService;

    public virtual bool StoredInHistory
    {
        get
        {
            return true;
        }
    }
    
    public virtual void OnBackPressed()
    {
        m_historyService.BackPressedItem(this);
    }

    public override void Show()
    {
        base.Show();
        if (StoredInHistory)
        {
            m_historyService.AddItem(this);
        }
    }

    public virtual void Restore()
    {
        Show();
    }

}
