using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MvvmCross.Core.ViewModels;
using MvvmCross.Wpf.Views;

namespace MagicalBaba.Desktop
{
    /// <summary>
    /// This custom presenter enables "back" navigation in WPF, allowing similar behavior to
    /// back actions in iOS, UWP, Droid.
    /// </summary>
    public class CustomViewPresenter : MvxSimpleWpfViewPresenter
    {
        #region Field Members
        // Simple stack for storing the navigation tree
        private readonly Stack<FrameworkElement> _navigationTree = new Stack<FrameworkElement>();
        #endregion Field Members

        #region Ctors
        public CustomViewPresenter(ContentControl contentControl) : base(contentControl) { }
        #endregion Ctors

        #region Methods - Public

        public override void Present(FrameworkElement frameworkElement)
        {
            _navigationTree.Push(frameworkElement);
            base.Present(frameworkElement);
        }

        public override void ChangePresentation(MvxPresentationHint hint)
        {
            // This will be null if hint isn't a close hint.
            var closeHint = hint as MvxClosePresentationHint;

            // Only navigate up the tree if the hint is an MvxClosePresentationHint
            // and there is a parent view in the navigation tree.
            if (closeHint == null || _navigationTree.Count <= 1) return;
            _navigationTree.Pop();
            Present(_navigationTree.Peek());
            base.ChangePresentation(hint);
        }
        #endregion Methods - Public
    }
}
