using System.Windows.Controls;

namespace NotationHelper.FlowTypes
{
    public static class MenuHelper 
    {
        public static void CreateSubMenu(this SubMenu menu, Action action, params string[] names) 
        {
            if (names.Length == 0)
                return;

            if(names.Length == 1) 
            {
                menu.Children.Add(new ActionItem() { Action = action, Name = names.First() });
                return;
            }

            var sub = menu.Children.Where(s => s is SubMenu).FirstOrDefault(ch => ch.Name.ToLower() == names.FirstOrDefault()?.ToLower()) as SubMenu;
            if(sub == null)
            {
                sub = new SubMenu() { Name = names.First() };
                menu.Children.Add(sub);
            }
            sub.CreateSubMenu(action, names.Skip(1).ToArray());
        }

        public static void CreateMenuGUI(Menu menu, SubMenu subMenu)
        {
            menu.Items.Clear();
            foreach(var sub in subMenu.Children)
            {
                if(sub is SubMenu)
                {
                    var createdItem = GetSubMenuItem(sub);
                    createdItem.Header = sub.Name;
                    menu.Items.Add(createdItem);
                }
                else if(sub is ActionItem actionItem)
                {
                    var acttionMenuItem = new MenuItem();

                    acttionMenuItem.Command = new DelegateCommand((o) => { actionItem.Action(); }, (o) => true);
                    acttionMenuItem.Header = actionItem.Name;
                    menu.Items.Add(acttionMenuItem);
                }
            }
        }

        public static MenuItem GetSubMenuItem(AMenuNode menuNode)
        {
            MenuItem menuItem = new MenuItem();
            if(menuNode is SubMenu subMenu)
            {
                foreach (var sub in subMenu.Children)
                {
                    var createdItem = GetSubMenuItem(sub);
                    createdItem.Header = sub.Name;
                    menuItem.Items.Add(createdItem);
                }
            }
            else
            {
                var actionItem = menuNode as ActionItem;
                menuItem.Command = new DelegateCommand((o) => { actionItem.Action(); }, (o) => true);
                menuItem.Header = actionItem.Name;
            }

            
            return menuItem;
        }
    }

}
