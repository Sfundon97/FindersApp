using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Grpc.Core;
using System;

namespace Finders.Models
{
    public class FirestoreConfig
    {
        public static FirestoreDb InitializeFirestore()
        {

            // Path to our service account key file
            var serviceAccountPath = "C:\\Users\\Sfundo Nondwatyu\\OneDrive\\Documents\\FindersApp\\findersmvc-9f3ec73a6f5e.json";

            // Load the credentials from the service account JSON file
            GoogleCredential googleCredential = GoogleCredential.FromFile(serviceAccountPath);

            // Create a ChannelCredentials object from the GoogleCredential
            ChannelCredentials channelCredentials = googleCredential.ToChannelCredentials();

            // Create the FirestoreClient instance
            FirestoreClient firestoreClient = new FirestoreClientBuilder
            {
                ChannelCredentials = channelCredentials
            }.Build();

            // Create the FirestoreDb instance
            FirestoreDb firestoreDb = FirestoreDb.Create("findersmvc", firestoreClient);

            return firestoreDb;
        }
    }
}
