﻿using LogQuake.CrossCutting.Cache;
using LogQuake.Domain.Context;
using LogQuake.Domain.Entities;
using LogQuake.Domain.Interfaces;
using LogQuake.Infra.CrossCuting;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogQuake.Infra.Data.Repositories
{
    public class KillRepository : RepositoryBase<Kill>, IKillRepository
    {
        /// <summary>
        /// Consutrtor da classe
        /// </summary>
        public KillRepository(LogQuakeContext context, IMemoryCache cache) : base(context, cache)
        {
            
        }

        /// <summary>
        /// Remover/Excluir todos os registro da tabela Kill
        /// </summary>
        public void RemoveAll()
        {
            context.Kills.RemoveRange(context.Kills);
        }

        /// <summary>
        /// Buscar todos os registros da tabela Kill de forma paginada
        /// </summary>
        /// <param name="pageRequest">Define a página e o tamanho da página a ser utilizada na consulta</param>
        /// <returns>
        /// Retornar uma lista de registro da tabela Kill.
        /// </returns>
        public new List<Kill> GetAll(PagingRequest pageRequest)
        {
            if (pageRequest == null)
            {
                throw new ArgumentNullException("KillRepository");
            }

            List<Kill> result = new List<Kill>();

            //Buscar jogos agrupados po IdGame
            var resultGroupByIdGame = context.Set<Kill>().OrderBy(i => i.IdGame).Select(p => new { p.IdGame }).GroupBy(i => i.IdGame)
                .Skip(((pageRequest.PageNumber - 1) * pageRequest.PageSize))
                .Take(pageRequest.PageSize)
                .ToList();

            if (resultGroupByIdGame.Count == 0)
            {
                return result;
            }

            //Criar uma lista com somente o campo IdGame
            List<int> temp = new List<int>();
            foreach (var item in resultGroupByIdGame)
            {
                temp.Add(item.Key);
            }
            if (temp.Count == 0)
            {
                return result;
            }

            result = context.Set<Kill>().Where(x => temp.Contains(x.IdGame)).ToList();

            return result;
        }

        /// <summary>
        /// Buscar registros da tabela Kill por Id
        /// </summary>
        /// <param name="Id">Identificador da tabela Kill</param>
        /// <returns>
        /// Retornar uma lista de registro da tabela Kill.
        /// </returns>
        public List<Kill> GetByIdList(int Id)
        {
            return context.Set<Kill>().Where(x => x.IdGame == Id).ToList();
        }


        /// <summary>
        /// Buscar primeiramente no Cache de Repositório registros da tabela Kill por Id e depois no Banco de Dados
        /// </summary>
        /// <param name="Id">Identificador da tabela Kill</param>
        /// <returns>
        /// Retornar uma lista de registro da tabela Kill.
        /// </returns>
        public List<Kill> GetCacheByIdList(int Id)
        {
            //Exemplo de como utilizar Cache em um Repositório
            var key = $"KillRepository.GetCacheByIdList{Id.ToString()}";
            List<Kill> retorno;

            if (!cache.TryGetValue(key, out retorno))
            {
                lock (Cache.lockCache) // ensure concurrent request won't access DB at the same time
                {
                    if (!cache.TryGetValue(key, out retorno)) // double-check
                    {
                        retorno = context.Set<Kill>().Where(x => x.IdGame == Id).ToList();
                        cache.Set(key, retorno,
                            new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(10), Size = 500 });
                    }
                }
            }

            return retorno;
        }
    }
}
