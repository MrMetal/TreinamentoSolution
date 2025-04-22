using Treinamento.Data.Context;
using Treinamento.Domain.Interfaces;
using Treinamento.Domain.Models;

namespace Treinamento.Data.Repositories;


public interface IEmpresaRepository : IRepository<Empresa>;

public class EmpresaRepository(ApplicationDbContext db) : Repository<Empresa>(db), IEmpresaRepository;