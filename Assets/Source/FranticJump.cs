namespace as3mbus.Selfish.Source
{
    public class FranticJump : Jump
    {
        protected void Update()
        {
            if (_rigidBd.velocity.y < 0)
                base.Action();
        }
    }
}