using Treinamento.Data.Context;
using Treinamento.Domain.Interfaces;
using Treinamento.Domain.Models;

namespace Treinamento.Data.Repositories;


public interface ISetorRepository : IRepository<Setor>;

public class SetorRepository(ApplicationDbContext db) : Repository<Setor>(db), ISetorRepository;