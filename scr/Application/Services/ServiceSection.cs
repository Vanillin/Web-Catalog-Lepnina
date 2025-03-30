using Application.Dto;
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

        public async Task<int?> Create(SectionDto element)
        {
            var mapElem = _mapper.Map<Section>(element);
            if (mapElem == null) return null;
            return await _repositSection.Create(mapElem); //id is changed later
        }
        public async Task<bool> Delete(int id) //must be transaction
        {
            SectionDto? element = await ReadById(id);
            if (element == null) return false;
            List<Product> memoryProduct = new List<Product>();

            try
            {
                var products = _repositProduct.ReadAll().Result.Where(x => x.IdSection == id).ToList();
                foreach (var v in products)
                {
                    var resultProduct = await _repositProduct.Delete(v.Id);
                    if (!resultProduct) throw new Exception();
                    memoryProduct.Add(v);
                }

                var result = await _repositSection.Delete(id);
                if (!result) throw new Exception();

                return true;
            }
            catch (Exception)
            {
                foreach (var v in memoryProduct)
                {
                    await _repositProduct.Create(v);
                }

                return false;
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
            if (element == null) return null;

            var mapElem = _mapper.Map<SectionDto>(element);
            return mapElem;
        }
        public async Task<bool> Update(SectionDto element)
        {
            var mapElem = _mapper.Map<Section>(element);
            if ( mapElem == null) return false;
            return await _repositSection.Update(mapElem);
        }
    }
}
