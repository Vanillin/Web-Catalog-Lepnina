using Application.Dto;
using Application.Exception;
using Application.Request;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class ServiceSection : IServiceSection
    {
        private IRepositSection _repositSection;
        private IRepositProduct _repositProduct;
        private IMapper _mapper;
        public ServiceSection(IRepositSection repositSection, IRepositProduct repositProduct, IMapper mapper)
        {
            _repositSection = repositSection;
            _repositProduct = repositProduct;
            _mapper = mapper;
        }

        public async Task<int?> Create(CreateSectionRequest request)
        {
            var result = await _repositSection.Create(new Section()
            {
                Name = request.Name,
            }
            );

            if (result == null) throw new EntityCreateException("Section is not create");
            return result;
        }
        public async Task<bool> Delete(int id) //must be transaction
        {
            SectionDto? element = await ReadById(id);
            if (element == null) throw new EntityNotFoundException("Section is not found");
            List<Product> memoryProduct = new List<Product>();

            try
            {
                var products = _repositProduct.ReadAll().Result.Where(x => x.IdSection == id).ToList();
                foreach (var v in products)
                {
                    var resultProduct = await _repositProduct.Delete(v.Id);
                    if (!resultProduct) throw new System.Exception();
                    memoryProduct.Add(v);
                }

                var result = await _repositSection.Delete(id);
                if (!result) throw new System.Exception();

                return true;
            }
            catch (System.Exception)
            {
                foreach (var v in memoryProduct)
                {
                    await _repositProduct.Create(v);
                }

                throw new EntityDeleteException("Something gone wrong");
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
            var result = await _repositSection.Update(new Section()
            {
                Id = request.Id,
                Name = request.Name,
            }
            );

            if (!result) throw new EntityUpdateException("Section is not updated");
            return true;
        }
    }
}
