using UnityEngine;
using System.Collections;
using Amazon.DynamoDBv2;
using Amazon.CognitoIdentity;
using Amazon.Runtime;
using Amazon;

namespace Area730.Amazon
{
    public class DynamoDbBase : MonoBehaviour
    {
        public string IdentityPoolId = "";

        private static IAmazonDynamoDB _ddbClient;

        private AWSCredentials _credentials;

        private AWSCredentials Credentials
        {
            get
            {
                if (_credentials == null)
                    _credentials = new CognitoAWSCredentials(IdentityPoolId, RegionEndpoint.USEast1);
                return _credentials;
            }
        }

        protected IAmazonDynamoDB Client
        {
            get
            {
                if (_ddbClient == null)
                {
                    _ddbClient = new AmazonDynamoDBClient(Credentials, RegionEndpoint.USEast1);
                }

                return _ddbClient;
            }
        }

    }
}
