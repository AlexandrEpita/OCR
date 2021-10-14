namespace tp14Ref
{
    public class Layer
    {
        public Neurone[] Neurones { get; }

        /// <summary>
        /// Create new layer of neurones
        /// </summary>
        /// <param name="size"> Dimension of the current layer </param>
        /// <param name="prevSize"> Dimension of the previous layer </param>
        public Layer(int size, int prevSize)
        {
            Neurones = new Neurone[size];

            for (var i = 0; i < size; i++)
                Neurones[i] = new Neurone(prevSize);
        }

        /// <summary>
        /// Mutate a layer
        /// </summary>
        /// <param name="layer"> Layer to mutate </param>
        /// <param name="mutate"> Apply mutation </param>
        public Layer(Layer layer, bool mutate)
        {
            Neurones = new Neurone[layer.Neurones.Length];
            for (var i = 0; i < layer.Neurones.Length; i++)
                Neurones[i] = new Neurone(layer.Neurones[i], mutate);
        }

        /// <summary>
        /// Mutate the current layer
        /// </summary>
        public void Mutate()
        {
            foreach (var neurone in Neurones)
                neurone.Mutate();
        }

        /// <summary>
        /// Mix the current layer with a partner layer
        /// </summary>
        /// <param name="partner"></param>
        public void Crossover(Layer partner)
        {
            for (var i = 0; i < Neurones.Length; i++)
                Neurones[i].Crossover(partner.Neurones[i]);
        }

        /// <summary>
        /// Apply the propagation function to the layer
        /// </summary>
        /// <param name="prevLayer"> Dimension of the previous layer</param>
        public void FrontProp(Layer prevLayer)
        {
            foreach (var neurone in Neurones)
                neurone.FrontProp(prevLayer);
        }
    }
}