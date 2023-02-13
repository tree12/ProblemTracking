using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProblemTracking.Entity;
using ProblemTracking.Entity.Entities;

namespace ProblemTracking.Repository
{
    public class UnitOfWork : IDisposable
    {
        public ApplicationDbContext context;
        private bool disposed = false;
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }
        private RepositoryBase<InvestigateStep> investigateStepRepository;
        private RepositoryBase<Machine> machineRepository; 
        private RepositoryBase<ProblemInvestigate> problemInvestigateRepository;
        private RepositoryBase<Problem> problemRepository;
        private RepositoryBase<User> userRepository;

        public RepositoryBase<InvestigateStep> InvestigateStepRepository
        {
            get
            {
                if (this.investigateStepRepository == null)
                {
                    this.investigateStepRepository = new InvestigateStepRepository(context);
                }
                return investigateStepRepository;
            }
        }

        public RepositoryBase<Machine> MachineRepository
        {
            get
            {
                if (this.machineRepository == null)
                {
                    this.machineRepository = new MachineRepository(context);
                }
                return machineRepository;
            }
        }
        public RepositoryBase<ProblemInvestigate> ProblemInvestigateRepository
        {
            get
            {
                if (this.problemInvestigateRepository == null)
                {
                    this.problemInvestigateRepository = new ProblemInvestigateRepository(context);
                }
                return problemInvestigateRepository;
            }
        }
        public RepositoryBase<Problem> ProblemRepository
        {
            get
            {
                if (this.problemRepository == null)
                {
                    this.problemRepository = new ProblemRepository(context);
                }
                return problemRepository;
            }
        }
        public RepositoryBase<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
