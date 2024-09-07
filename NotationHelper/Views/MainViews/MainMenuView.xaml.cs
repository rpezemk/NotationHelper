﻿using NotationHelper.FlowTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NotationHelper.Views.MainViews
{
    /// <summary>
    /// Logika interakcji dla klasy MainMenuView.xaml
    /// </summary>
    public partial class MainMenuView : UserControl
    {
        public MainMenuView()
        {
            InitializeComponent();
            var virtualMenu = ProgramSettings.GetMainMenu();
            MenuHelper.CreateMenuGUI(MainMenu, virtualMenu);
        }
    }
}
