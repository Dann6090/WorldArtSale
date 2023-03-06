using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassArt : ClassNotify
    {
        private int _id; 
        private string _picturePath;
        private string _description;
        private string _titel;
        private bool _isActive;

        public ClassArt()
        {
            id = 0;
            picturePath = string.Empty;
            description = string.Empty;
            titel = string.Empty;
            isActive = true;
        }
        public ClassArt(ClassArt inArt)
        {
            id = inArt.id;
            picturePath = inArt.picturePath;
            description = inArt.description;
            titel = inArt.titel; 
            isActive = inArt.isActive;
        }
        public ClassArt(string inPath, string inDes, string inTitel)
        {
            id = 0;
            picturePath = inPath;
            description = inDes;
            titel = inTitel;
            isActive = false;
        }
        public ClassArt(int inId, string inPath, string inDes, string inTitel, bool inActive)
        {
            id = inId;
            picturePath = inPath;
            description = inDes;
            titel = inTitel;
            isActive = inActive;
        }
        public int id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                }
                Notify("id");
            }
        }
        public string picturePath
        {
            get { return _picturePath; }
            set
            {
                if (_picturePath != value)
                {
                    _picturePath = value;
                }
                Notify("picturePath");
            }
        }
        public string description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                }
                Notify("description");
            }
        }
        public string titel
        {
            get { return _titel; }
            set
            {
                if (_titel != value)
                {
                    _titel = value;
                }
                Notify("titel");
            }
        }
        public bool isActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                }
                Notify("isActive");
            }
        }

    }
}
