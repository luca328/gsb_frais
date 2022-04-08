using GSBFrais.Model.data;
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

namespace WPFFrais
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class AddFrais2000 : Window
    {
        private DaoEtat unDaoEtat;

        public AddFrais2000(DaoFicheFrais uneDaoFicheFrais, DaoEtat unDaoEtat, DaoLigneFraisHorsForfait unDaoLigneFraisHorsForfait)
        {
            InitializeComponent();
            mainGrid.DataContext = new viewModel.ViewModelFicheFrais(uneDaoFicheFrais, unDaoEtat, unDaoLigneFraisHorsForfait);
        }
    }
}
