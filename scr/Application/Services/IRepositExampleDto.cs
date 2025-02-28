using Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IRepositExampleDto
    {
        Task<ExampleDto> ReadById(int id);
        Task<List<ExampleDto>> ReadAll();
        Task Create(ExampleDto element);
        Task<bool> Update(ExampleDto element);
        Task<bool> Delete(int id);
    }
}
