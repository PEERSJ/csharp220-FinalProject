using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PartRepository;

namespace PartApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static PartRepository.PartRepository partRepository;

        static App()
        {
            partRepository = new PartRepository.PartRepository();
        }

        public static PartRepository.PartRepository PartRepository
        {
            get
            {
                return partRepository;
            }
        }
    }
}
