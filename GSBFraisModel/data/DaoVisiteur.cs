using GSBFrais.Model.business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSBFrais.Model.data
{
    public class DaoVisiteur
    {
        private Dbal _dbal;

        public DaoVisiteur(Dbal mydbal)
        {
            this._dbal = mydbal;
        }

        public void Insert(Visiteur _visiteur)
        {
            string query = " visiteur(id, name) VALUES(" +_visiteur.Id + ",'" + _visiteur.Nom.Replace(" '", "''") + "')";
            this._dbal.Insert(query);
        }
        public List<Visiteur> SelectAll()
        {
            List<Visiteur> listVisiteur = new List<Visiteur>();
            DataTable myTable = this._dbal.SelectAll("visiteur");
            foreach (DataRow r in myTable.Rows)
            {

                listVisiteur.Add(new Visiteur(r[" id "].ToString(), (string)r[" nom "], (string)r[" prenom "], (string)r[" login "], (string)r[" mdp "], (string)r[" adresse "], (string)r[" cp "], (string)r[" ville "], (DateTime)r[" dateEmbauche "]));
            }
            return listVisiteur;
        }
        public Visiteur SelectByName(string nomVisiteur)
        {
            DataTable result = new DataTable();
            result = this._dbal.SelectByField("visiteur", "nom = " + nomVisiteur.Replace("'","''") + "'");
            Visiteur foundVisiteur = new Visiteur(result.Rows[0]["id "].ToString(),(string)result.Rows[0]["nom"], (string)result.Rows[0]["prenom"], (string)result.Rows[0]["login"], (string)result.Rows[0]["mdp"], (string)result.Rows[0]["adresse"], (string)result.Rows[0]["cp"], (string)result.Rows[0]["ville"], (DateTime)result.Rows[0]["dateEmbauche"]);
            return foundVisiteur;
        }
        public Visiteur SelectById(string idVisiteur)
        {
            DataRow result = this._dbal.SelectById("Visiteur", idVisiteur);
            return new Visiteur(result["id"].ToString(), (string)result["nom"], (string)result["prenom"], (string)result["login"], (string)result["mdp"], (string)result["adresse"], (string)result["cp"], (string)result["ville"], (DateTime)result["dateEmbauche"]);
        }

        public void Delete(Visiteur _visiteur)
        {
            string query = "visiteur WHERE id = " + _visiteur.Id;
            this._dbal.delete(query);
        }
        public void Update(Visiteur _visiteur)
        {
            string query = "visiteur set nom = " + _visiteur.Nom.Replace("'", "''") + ",'" + "prenom = " + _visiteur.Prenom.Replace("'", "''") + ",'" + "login = " + _visiteur.Login.Replace("'", "''") +  "where id = " + _visiteur.Id;
            this._dbal.update(query);
        }
    }
}
