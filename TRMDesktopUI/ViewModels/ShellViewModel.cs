using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.EventModels;

namespace TRMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private SalesViewModel _salesVM;
        private IEventAggregator _eventAggregator;
        private SimpleContainer _container;
        public ShellViewModel(IEventAggregator eventAggregator, SalesViewModel salesVM, SimpleContainer container)
        {
            _eventAggregator = eventAggregator;
            _salesVM = salesVM;
            _container = container;
            _eventAggregator.Subscribe(this);
            ActivateItem(container.GetInstance<LoginViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
        }
    }
}
