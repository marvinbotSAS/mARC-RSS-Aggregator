namespace mARC
{
    using System;

    public class ASCIITable
    {
        private Connector _connector;

        public ASCIITable(Connector connector)
        {
            this._connector = connector;
        }

        public void Create(string name, string data)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.localParams.Clear();
            this._connector.Push("ASCIITable.Create");
            this._connector.Push(name);
            this._connector.Push(data);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }

        public void GetInstances()
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.localParams.Clear();
            this._connector.Push("ASCIITable.Get");
            this._connector.Push("Instances");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }

        public void Kill(string name)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.localParams.Clear();
            this._connector.Push("ASCIITable.Kill");
            this._connector.Push(name);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }

        public void Rename(string name, string newname)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.localParams.Clear();
            this._connector.Push("ASCIITable.Rename");
            this._connector.Push(name);
            this._connector.Push(newname);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
    }
}

