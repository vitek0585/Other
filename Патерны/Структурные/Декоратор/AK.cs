using System;

namespace DecorAK
{
    public abstract class AKBase
    {

        protected string _name;
        protected int _coast;
        protected int _mode;
        public abstract void Shoot();
        public virtual void ModeShoot(int mode)
        {
            _mode = mode;
        }

        public abstract string Desccription();
        public abstract int Coast();
    }
    public abstract class AKDecorator : AKBase
    {
        protected AKBase _content;
    }

    public class PodstvolAK : AKDecorator
    {
        public PodstvolAK(AKBase content):this()
        {
            _content = content;
        }

        public PodstvolAK()
        {
            _name = "Офигенный подствол";
            _coast = 230;
        }
        public override string Desccription()
        {
            if(_content!=null)
            return string.Format("К автомату {0} добавлен {1}",_content.Desccription().ToLower(),_name.ToLower());
            return _name;

        }

        public override int Coast()
        {
            if(_content!=null)
                return _coast + _content.Coast();

            return _coast;
        }

        public override void Shoot()
        {
            if (_mode == 1)
           Console.WriteLine("Гашу с подствола");
            else
                _content.Shoot();
            
        }
        
    }
    public class AK:AKBase
    {
        public AK()
        {
            _name = "Обычный автомат АК 47";
            _coast = 350;
        }
        public override string Desccription()
        {
            return _name;
        }

        public override int Coast()
        {
            return _coast;
        }

        public override void Shoot()
        {
            Console.WriteLine("Стреляю одиночными");
        }

    }
}