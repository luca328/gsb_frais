using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSBFrais.Model.business;
using System.Data;

namespace GSBFrais.Model.data
{
    public class DaoEtat
    {
        private Dbal _dbal;
        public DaoEtat(Dbal mydbal)
        {
            this._dbal = mydbal;
        }
        public List<Etat> SelectAll()
        {
            List<Etat> listeEtats = new List<Etat>();
            DataTable maTable = this._dbal.SelectAll("etat");
            foreach (DataRow r in maTable.Rows)
            {
                listeEtats.Add(new Etat((string)r["id"], (string)r["libelle"]));
            }
            return listeEtats;
        }

        public Etat SelectByIdEtat(string idEtat)
        {
            DataTable resultat = this._dbal.SelectByField("etat", "id = '" + idEtat + "'");
            DataRow dataRow = resultat.Rows[0];
            Etat trouverEtat = new Etat((string)dataRow["id"], (string)dataRow["libelle"]);
            return trouverEtat;
        }
    }
}
