using Treinamento.Data.Context;
using Treinamento.Domain.Interfaces;
using Treinamento.Domain.Models;

namespace Treinamento.Data.Repositories;

public interface IEnderecoRepository : IRepository<Endereco>;

public class EnderecoRepository(ApplicationDbContext db) : Repository<Endereco>(db), IEnderecoRepository;