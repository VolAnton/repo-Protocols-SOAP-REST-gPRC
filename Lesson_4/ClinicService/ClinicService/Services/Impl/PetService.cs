using ClinicService.Data;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using PetServiceNamespace;
using System;
using static PetServiceNamespace.PetService;

namespace ClinicService.Services.Impl
{
    public class PetService : PetServiceBase
    {
        private readonly ClinicServiceDbContext _dbContext;

        public PetService(ClinicServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override Task<CreatePetResponse> CreatePet(CreatePetRequest request, ServerCallContext context)
        {
            try
            {
                var pet = new Pet
                {
                    ClientId = request.ClientId,
                    Name = request.Name,
                    Birthday = request.Birthday.ToDateTime()
                };

                _dbContext.Pets.Add(pet);
                _dbContext.SaveChanges();

                var response = new CreatePetResponse
                {
                    PetId = pet.Id,
                    ErrCode = 0,
                    ErrMessage = ""
                };

                return Task.FromResult(response);
            }
            catch (Exception e)
            {
                var response = new CreatePetResponse
                {
                    ErrCode = 2001,
                    ErrMessage = "Internal server error."
                };

                return Task.FromResult(response);
            }
        }

        public override Task<GetPetsResponse> GetPetsByClientId(GetPetsRequest request, ServerCallContext context)
        {
            try
            {
                var response = new GetPetsResponse();

                var pets = _dbContext.Pets.Where(p => p.ClientId == request.ClientId).Select(pet => new PetResponse
                {
                    PetId = pet.Id,
                    ClientId = pet.ClientId,
                    Name = pet.Name,
                    Birthday = Timestamp.FromDateTime(pet.Birthday.ToUniversalTime())
                }).ToList();

                response.Pets.AddRange(pets);
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                var response = new GetPetsResponse
                {
                    ErrCode = 2002,
                    ErrMessage = "Internal server error."
                };

                return Task.FromResult(response);
            }
        }

    }
}
