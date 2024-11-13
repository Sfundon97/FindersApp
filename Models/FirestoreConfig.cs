using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Finders.Models
{
    public class FirestoreConfig
    {
        private readonly IConfiguration _configuration;

        public FirestoreConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public FirestoreDb InitializeFirestore()
        {
            // Get the service account key path from configuration
            var serviceAccountPath = _configuration["Firebase:ServiceAccountKeyPath"];
            if (string.IsNullOrEmpty(serviceAccountPath))
            {
                throw new InvalidOperationException("Service account key path is not configured.");
            }

            // Convert to absolute path if necessary
            serviceAccountPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, serviceAccountPath);

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
