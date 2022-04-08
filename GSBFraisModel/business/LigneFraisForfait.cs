using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSBFrais.Model.business
{
    public class LigneFraisForfait
    {
        private FraisForfait _fraisForfait;
        private FicheFrais _ficheFrais;
        private int _quantite;
        public LigneFraisForfait(FraisForfait unFraisForfait, int uneQuantite, FicheFrais uneFicheFrais)
        {
            this._fraisForfait = unFraisForfait;
            this._quantite = uneQuantite;
            this._ficheFrais = uneFicheFrais;
        }

        public int Quantite
        {
            get
            {
                return _quantite;
            }

            set
            {
                _quantite = value;
            }
        }
        public FraisForfait FraisForfait
        {
            get
            {
                return _fraisForfait;
            }

            set
            {
                _fraisForfait = value;
            }
        }
        public FicheFrais FicheFrais
        {
            get
            {
                return _ficheFrais;
            }

            set
            {
                _ficheFrais = value;
            }
        }
    }
}
