using Grpc.Net.Client;
using ClinicServiceNamespace;
using static ClinicServiceNamespace.ClinicService;
using PetServiceNamespace;
using static PetServiceNamespace.PetService;
using Google.Protobuf.WellKnownTypes;

namespace ClinicClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            using var channel = GrpcChannel.ForAddress("http://localhost:5001");

            #region Client creation

            ClinicServiceClient clinicServiceClient = new ClinicServiceClient(channel);

            var createClientResponse = clinicServiceClient.CreateClinet(new CreateClientRequest
            {
                Document = "DOC 32167",
                FirstName = "Волощук",
                Patronymic = "Антон",
                Surname = "Анатольевич"
            });

            if (createClientResponse.ErrCode == 0)
            {
                Console.WriteLine($"Client #{createClientResponse.ClientId} created successfully.");
            }
            else
            {
                Console.WriteLine($"Create client error.\nErrorCode: {createClientResponse.ErrCode}\nErrorMessage: {createClientResponse.ErrMessage}");
            }

            #endregion

            #region Get Clients list
            var getClientResponse = clinicServiceClient.GetClients(new GetClientsRequest());

            if (createClientResponse.ErrCode == 0)
            {
                Console.WriteLine("Clients");
                Console.WriteLine("=======\n");

                foreach (var client in getClientResponse.Clients)
                {
                    Console.WriteLine($"#{client.ClientId} {client.Document} {client.Surname} {client.FirstName} {client.Patronymic}");
                }
            }
            else
            {
                Console.WriteLine($"Get clients error.\nErrorCode: {getClientResponse.ErrCode}\nErrorMessage: {getClientResponse.ErrMessage}");
            }

            #endregion

            #region Pet creation

            PetServiceClient petServiceClient = new PetServiceClient(channel);

            var createPetResponse = petServiceClient.CreatePet(new CreatePetRequest
            {
                ClientId = 7,
                Name = "Бобик",
                Birthday = Timestamp.FromDateTime(new DateTime(2022, 01, 01).ToUniversalTime())
            });

            if (createPetResponse.ErrCode == 0)
            {
                Console.WriteLine($"Pet #{createPetResponse.PetId} created successfully.");
            }
            else
            {
                Console.WriteLine($"Create pet error.\nErrorCode: {createPetResponse.ErrCode}\nErrorMessage: {createPetResponse.ErrMessage}");
            }

            #endregion

            #region Get Pets By Client Id
            var getPetResponse = petServiceClient.GetPetsByClientId(new GetPetsRequest
            {
                ClientId = 7
            });

            if (createPetResponse.ErrCode == 0)
            {
                Console.WriteLine("Pets");
                Console.WriteLine("=======\n");

                foreach (var pet in getPetResponse.Pets)
                {
                    Console.WriteLine($"#{pet.PetId} {pet.ClientId} {pet.Name} {pet.Birthday}");
                }
            }
            else
            {
                Console.WriteLine($"Get clients error.\nErrorCode: {getPetResponse.ErrCode}\nErrorMessage: {getPetResponse.ErrMessage}");
            }

            #endregion

            Console.ReadKey();

        }
    }
}