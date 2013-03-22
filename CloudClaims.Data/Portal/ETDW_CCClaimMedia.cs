using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudClaims.Model;
using System.Data;

namespace CloudClaims.Data.Portal {
    public static class ETDW_CCClaimMedia {

        public static CCClaimMedia GetClaimMediaByID(int client, int mediaID) {
            var db = new CCPortalEntities();
            CCClaimMedia media = new CCClaimMedia();
            media = db.CCClaimMedias.Find(mediaID);
            if (media.clientID == client) {
                return media;
            } else return null;
        }

        public static IEnumerable<CCClaimMedia> GetMediaForClaim(int clientID, int claimID) {
            var db = new CCPortalEntities();
            return db.CCClaimMedias.Where(x => x.clientID == clientID && x.claimID == claimID);
        }

        public static int AddClaimMedia(int clientID, int claimID, int uploadedByPersonID, string mediaType, string title, string description, string filename) {
            //https://s3.amazonaws.com/cloudclaims-media/prod/479F1A05-8318-4A09-8521-2CDD3EBC19E2/17/traffic_accident.jpg
            string location = String.Format("{0}/{1}/{2}/{3}",
                System.Configuration.ConfigurationSettings.AppSettings["CloudFiles"].ToString(),
                EverythingToDoWith_CCClient.getClientByID(clientID).clientGUID,
                claimID.ToString(),
                filename);
            var db = new CCPortalEntities();
            CCClaimMedia item = new CCClaimMedia {
                clientID = clientID,
                claimID = claimID,
                uploadedByPersonID = uploadedByPersonID,
                mediaType = mediaType,
                title = title,
                description = description,
                fileName = filename,
                fileLocation = location
            };
            db.CCClaimMedias.Add(item);
            db.SaveChanges();
            return item.mediaID;
        }

        public static void UpdateClaimMedia(int clientID, int claimID, int mediaID, string mediaType, string title, string description) {
            var db = new CCPortalEntities();
            CCClaimMedia m = db.CCClaimMedias.Where(x => x.clientID == clientID && x.claimID == claimID && x.mediaID == mediaID).SingleOrDefault();
            if (m != null) {
                m.mediaType = mediaType;
                m.title = title;
                m.description = description;
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void DeleteClaimMedia(int clientID, int claimID, int mediaID) {
            var db = new CCPortalEntities();
            CCClaimMedia m = db.CCClaimMedias.Where(x => x.clientID == clientID && x.claimID == claimID && x.mediaID == mediaID).SingleOrDefault();
            if (m != null) {
                m.deletedFlag = true;
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
