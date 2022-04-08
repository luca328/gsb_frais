using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSBFrais.Model.business
{
    public class FraisForfait
    {
        string _id;
        string _libelle;
        decimal _montant;

        public FraisForfait(string unId, string unLibelle, decimal unMontant)
        {
            this._id = unId;
            this._libelle = unLibelle;
            this._montant = unMontant;
        }

        public string Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public string Libelle
        {
            get
            {
                return _libelle;
            }

            set
            {
                _libelle = value;
            }
        }

        public decimal Montant
        {
            get
            {
                return _montant;
            }

            set
            {
                _montant = value;
            }
        }
    }
}
