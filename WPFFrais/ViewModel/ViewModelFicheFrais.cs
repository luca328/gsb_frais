using GSBFrais.Model.business;
using GSBFrais.Model.data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFFrais.viewModel
{
    class ViewModelFicheFrais : ViewModelBase
    {
        private ObservableCollection<FicheFrais> _listFicheFrais;
        private DaoFicheFrais _vmDaoFicheFrais;
        private DaoLigneFraisHorsForfait _vmDaoLigneFraisHorsForfait;
        private ObservableCollection<string> _listMois;
        private ObservableCollection<Etat> _listEtat;
        private Etat _selectedEtat;
        private string _selectedMonth;
        private FicheFrais _selectedFicheFrais;
        private LigneFraisHorsForfait _selectedFraisHorsForfait;
        private string _etape;
        private string _kilometre;
        private string _nuit;
        private string _repas;
        private ObservableCollection<LigneFraisHorsForfait> _listLigneFraisHorsForfait;
        private bool canExecute = true;
        private ICommand _reportFrais;
        private ICommand _deleteFraisHorsForfait;
        private ICommand _applyChange;

        public ObservableCollection<FicheFrais> ListFicheFrais
        {
            get
            {
                return _listFicheFrais;
            }
            set
            {
                this._listFicheFrais = value;
                OnPropertyChanged("ListFicheFrais");
            }
        }
        public ObservableCollection<string> ListMois
        {
            get
            {
                return _listMois;
            }
            set
            {
                this._listMois = value;
            }
        }

        public string SelectedMonth
        {
            get
            {
                return _selectedMonth;
            }

            set
            {
                _selectedMonth = value;
                ListFicheFrais = new ObservableCollection<FicheFrais>(_vmDaoFicheFrais.SelectByMonth(_selectedMonth));
            }
        }

        public FicheFrais SelectedFicheFrais
        {
            get
            {
                return _selectedFicheFrais;
            }

            set
            {
                _selectedFicheFrais = value;

                if (_selectedFicheFrais != null)
                {
                    foreach(LigneFraisForfait laLigneFraisForfait in this._selectedFicheFrais.LesLignesFraisForfait)
                    {
                        switch (laLigneFraisForfait.FraisForfait.Id)
                        {
                            case "ETP":
                                Etape = laLigneFraisForfait.Quantite.ToString();
                                break;
                            case "KM":
                                Kilometre = laLigneFraisForfait.Quantite.ToString();
                                break;
                            case "NUI":
                                Nuit = laLigneFraisForfait.Quantite.ToString();
                                break;
                            case "REP":
                                Repas = laLigneFraisForfait.Quantite.ToString();
                                break;
                        }
                    }
                    SelectedEtat = SelectedListEtat(_selectedFicheFrais.UnEtat);
                    ListLigneFraisHorsForfait = new ObservableCollection<LigneFraisHorsForfait>(_vmDaoLigneFraisHorsForfait.SelectByFicheFrais(_selectedFicheFrais));
                }
            }
        }

        public string Etape
        {
            get
            {
                return _etape;
            }

            set
            {
                _etape = value;
                OnPropertyChanged("Etape");

            }
        }

        public string Kilometre
        {
            get
            {
                return _kilometre;
            }

            set
            {
                _kilometre = value;
                OnPropertyChanged("Kilometre");
            }
        }

        public string Nuit
        {
            get
            {
                return _nuit;
            }

            set
            {
                _nuit = value;
                OnPropertyChanged("Nuit");
            }
        }

        public string Repas
        {
            get
            {
                return _repas;
            }

            set
            {
                _repas = value;
                OnPropertyChanged("Repas");
            }
        }


        public ObservableCollection<Etat> ListEtat
        {
            get
            {
                return _listEtat;
            }

            set
            {
                _listEtat = value;
            }
        }

        public Etat SelectedEtat
        {
            get
            {
                return _selectedEtat;
            }

            set
            {
                _selectedEtat = value;
                _selectedFicheFrais.UnEtat = _selectedEtat;
                OnPropertyChanged("SelectedEtat");
            }
        }

        public ObservableCollection<LigneFraisHorsForfait> ListLigneFraisHorsForfait
        {
            get
            {
                return _listLigneFraisHorsForfait;
            }

            set
            {
                _listLigneFraisHorsForfait = value;
                OnPropertyChanged("ListLigneFraisHorsForfait");
            }
        }

        public Etat SelectedListEtat(Etat unEtat)
        {
            Etat etatSelected = unEtat;
            foreach(Etat e in _listEtat)
            {
                if (unEtat.Id == e.Id)
                {
                    etatSelected = e;
                }
            }
            return etatSelected;
        }

        public ICommand ApplyChange
        {
            get
            {
                return _applyChange ?? (_applyChange = new RelayCommand(() => UpdateFicheFrais(), () => canExecute));
            }
        }

        public ICommand DeleteFraisHorsForfait
        {
            get
            {
                return _deleteFraisHorsForfait ?? (_deleteFraisHorsForfait = new RelayCommand(() => DeleteFraisHorsForfaitAction(), () => canExecute));
            }
        }

        public LigneFraisHorsForfait SelectedFraisHorsForfait
        {
            get
            {
                return _selectedFraisHorsForfait;
            }

            set
            {
                _selectedFraisHorsForfait = value;
                OnPropertyChanged("SelectedFraisHorsForfait");
            }
        }

        private void DeleteFraisHorsForfaitAction()
        {
            _vmDaoLigneFraisHorsForfait.Delete(_selectedFraisHorsForfait);
            ListLigneFraisHorsForfait.Remove(_selectedFraisHorsForfait);
        }

        public ICommand ReportFrais
        {
            get
            {
                return _reportFrais ?? (_reportFrais = new RelayCommand(() => ReportFraisAction(), () => true));
            }
        }

        private void ReportFraisAction()
        {
            string moisActuel;
            if (DateTime.Now.Month < 10)
            {
                moisActuel = DateTime.Now.Year.ToString() + '0' + DateTime.Now.Month.ToString();
            }
            else
            {
                moisActuel = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
            }

            FicheFrais ficheFrais = _vmDaoFicheFrais.SelectByVisiteurMois(_selectedFicheFrais.UnVisiteur, moisActuel);

            if (ficheFrais == null)
            {
                ficheFrais = new FicheFrais(moisActuel, _selectedFicheFrais.NbJustificatifs, _selectedFicheFrais.MontantValide, _selectedFicheFrais.Datemodif, _selectedFicheFrais.UnVisiteur, _selectedFicheFrais.UnEtat);
                _vmDaoFicheFrais.Insert(ficheFrais);
            }
            _selectedFraisHorsForfait.Fichefrais = ficheFrais;
            _vmDaoLigneFraisHorsForfait.Update(_selectedFraisHorsForfait);
            ListLigneFraisHorsForfait.Remove(_selectedFraisHorsForfait);
            _selectedFicheFrais.LesLignesFraisHorsForfaits.Remove(_selectedFraisHorsForfait);

        }

        public void UpdateFicheFrais()
        {
            _vmDaoFicheFrais.Update(_selectedFicheFrais);
        }

        public ViewModelFicheFrais(DaoFicheFrais uneDaoFicheFrais, DaoEtat unDaoEtat, DaoLigneFraisHorsForfait unDaoLigneFraisHorsForfait)
        {
            this._vmDaoFicheFrais = uneDaoFicheFrais;
            _listFicheFrais = new ObservableCollection<FicheFrais>(_vmDaoFicheFrais.SelectAll());
            _listMois = new ObservableCollection<string>(_vmDaoFicheFrais.SelectListMois());
            _listEtat = new ObservableCollection<Etat>(unDaoEtat.SelectAll());
            _vmDaoLigneFraisHorsForfait = unDaoLigneFraisHorsForfait;
        }
    }
}
