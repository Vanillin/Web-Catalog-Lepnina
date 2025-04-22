using Application.Dto;
using Application.Exception;
using Application.Request;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Application.Services
{
    public class ServiceSection : IServiceSection
    {
        private IRepositSection _repositSection;
        private IRepositProduct _repositProduct;
        private IMapper _mapper;
        private NpgsqlConnection _connection;
        private ILogger<ServiceSection> _logger;
        public ServiceSection(IRepositSection repositSection, IRepositProduct repositProduct, IMapper mapper, NpgsqlConnection connection, ILogger<ServiceSection> logger)
        {
            _repositSection = repositSection;
            _repositProduct = repositProduct;
            _mapper = mapper;
            _connection = connection;
            _logger = logger;
        }

        public async Task<int?> Create(CreateSectionRequest request)
        {
            var result = await _repositSection.Create(new Section()
            {
                Name = request.Name,
            }
            );

            if (result == null) throw new EntityCreateException("Section is not created");

            _logger.LogInformation($"Section created with id {result}");
            return result;
        }
        public async Task<bool> Delete(int id)
        {
            await _connection.OpenAsync();

            await using (var tran = _connection.BeginTransaction())
            {
                try
                {
                    var products = _repositProduct.ReadAll().Result.Where(x => x.IdSection == id).ToList();
                    foreach (var v in products)
                    {
                        var resultProduct = await _repositProduct.Delete(v.Id);
                        if (!resultProduct) throw new EntityDeleteException("Product is not deleted");
                    }

                    var result = await _repositSection.Delete(id);
                    if (!result) throw new EntityDeleteException("Section is not deleted");

                    tran.Commit();

                    _logger.LogInformation($"Deleted section with id {id} and all connections to it");
                    return true;
                }
                catch (BaseApplicationException)
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    await _connection.CloseAsync();
                }
            }
        }
        public async Task<IEnumerable<SectionDto>> ReadAll()
        {
            var allElem = await _repositSection.ReadAll();
            var mapAllElem = allElem.Select(q => _mapper.Map<SectionDto>(q)).ToList();
            return mapAllElem;
        }
        public async Task<SectionDto?> ReadById(int id)
        {
            var element = await _repositSection.ReadById(id);
            if (element == null) throw new EntityNotFoundException("Section is not found");

            var mapElem = _mapper.Map<SectionDto>(element);
            return mapElem;
        }
        public async Task<bool> Update(UpdateSectionRequest request)
        {
            var element = await _repositSection.ReadById(request.Id);
            if (element == null) throw new EntityNotFoundException("Section is not found");

            element.Name = request.Name;

            var result = await _repositSection.Update(element);

            if (!result) throw new EntityUpdateException("Section is not updated");

            _logger.LogInformation($"Section updated with id {element.Id}");
            return true;
        }
    }
}
