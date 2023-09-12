using System;

namespace SavegameToolkit
{
    internal class ArkCryoStore : GameObjectContainerMixin
    {
        private long propertiesOffset;

        public ArkCryoStore(ArkArchive archive)
        {
            ReadBinary(archive);
        }

        public void ReadBinary(ArkArchive archive)
        {
            // check object type
            if (archive.ReadString().ToLowerInvariant() != "dino") return;

            var stringPropertyCount = archive.ReadInt();
            while (stringPropertyCount-- > 0)
            {
                archive.ReadString(); // e.g. creature info (species, name, tribe, etc.)
            }

            var floatPropertyCount = archive.ReadInt();
            while (floatPropertyCount-- > 0)
            {
                archive.ReadFloat(); // e.g. stats
            }

            var colorNameCount = archive.ReadInt(); // color name count
            while (colorNameCount-- > 0)
            {
                var colorName = archive.ReadString();
            }

            var cryoStoreUnknown1 = archive.ReadLong();

            // store properties offset
            propertiesOffset = archive.Position;

            // load GameObjects
            Objects.Clear();

            bool useNameTable = archive.UseNameTable;
            archive.UseNameTable = false;

            var objectCount = archive.ReadInt();
            while (objectCount-- > 0)
            {
                Objects.Add(new GameObject(archive));
            }

            archive.UseNameTable = useNameTable;
        }

        public void LoadProperties(ArkArchive archive)
        {
            bool useNameTable = archive.UseNameTable;
            archive.UseNameTable = false;

            var pos = archive.Position;

            for (int i = 0; i < Objects.Count; i++)
            {
                Objects[i].LoadProperties(archive, new GameObject(), (int)propertiesOffset);
            }

            archive.Position = pos;
            archive.UseNameTable = useNameTable;
        }

    }
}
