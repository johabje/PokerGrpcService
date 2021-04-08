using System;
namespace PokerGrpc.Singelton
{
    public class StorageSingleton
    {
        private static readonly StorageSingleton instance;

        static StorageSingleton()
        {
            instance = new StorageSingleton();
        }
        // Mark constructor as private as no one can create it but itself.
        private StorageSingleton()
        {
            this.currentGames = new System.Collections.Generic.List<Models.PokerGame>();
        }

        // The only way to access the created instance.
        public static StorageSingleton Instance
        {
            get
            {
                return instance;
            }
        }

        // Note that this will be null when the instance if not set to
        // something in the constructor.
        public System.Collections.Generic.List<Models.PokerGame> currentGames { get; set; }
    }
}
