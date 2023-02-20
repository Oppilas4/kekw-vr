namespace Kekw.info
{
    /*
    Tähän kansioon tulee kaikki koodi.
    Käytettävä kieli on ENGLANTI!
    Poistan kaikki tiedostot, jotka ei ole englantia!
    Nimeä asiat oikein!
    Käytä namespace!

    private muuttujien nimet alkaa _.
    public muuttujien nimet alkaa pienellä kirjaimella
    public ja private property nimet alkaa isolla kirjaimella.

    interface luokan nimi alkaa I esim IDoSomething
    Abstract luokan nimi alkaa A esim ADoSomething
    Base luokaksi suunniteltu luokka alkaa B esim BExample

    KOODI ja kommentointi esimerkki
    */

   
    /// <summary>
    /// Every object who is able to calculate implements this interface.
    /// </summary>
    interface ICalulate
    {
        /// <summary>
        /// Multiplies value by two and converts it to double.
        /// </summary>
        /// <returns></returns>
        public double CalculateDouble();
    }

    /// <summary>
    /// This is example class and it does show you how class should be documented.
    /// Class summary should tell what class does.
    /// </summary>
    internal class BExample: ICalulate
    {
        // You can give short description, not required.
        private int _totalReadCount;

        /// <summary>
        /// Contains value of how many times this document is read.
        /// Properties must be documented.
        /// </summary>
        public int TotalReadCount { get => _totalReadCount; private set => _totalReadCount = value; }


        /// <summary>
        /// Base constructor that initializes <see cref="TotalReadCount"/> to zero.
        /// </summary>
        internal BExample()
        {
            this._totalReadCount = 0;
        }

        /// <summary>
        /// Build new Example class, requires initial value for <see cref="TotalReadCount"/>.
        /// </summary>
        /// <param name="initialReadCount">Initial value for <see cref="TotalReadCount"/></param>
        internal BExample(int initialReadCount)
        {
            this._totalReadCount = initialReadCount;
        }

        /// <summary>
        /// Increment <see cref="_totalReadCount"/> by one.
        /// </summary>
        public void IncrementReadCount()
        {
            this._totalReadCount++;
        }

        /// <summary>
        /// Method sets <see cref="_totalReadCount"/> to <see cref="fixedValue"/>.
        /// </summary>
        /// <param name="fixedValue"></param>
        public void FixReadCount(int fixedValue)
        {
            this._totalReadCount = fixedValue;
        }

        /// <summary>
        /// <see cref="ICalulate.CalculateDouble"/>
        /// </summary>
        /// <returns></returns>
        public double CalculateDouble()
        {
            return (double)this._totalReadCount * 2;
        }
    }

    /// <summary>
    /// Class expands <seealso cref="BExample"/> functionality with review counter;
    /// </summary>
    internal class Example2: BExample
    {
        private int _reviewCount;

        /// <summary>
        /// Base constructor sets <see cref="_reviewCount"/> to parameter and base readcount to 0;
        /// </summary>
        /// <param name="reviewCount">How many reviews are initially</param>
        public Example2(int reviewCount): base()
        {
            this._reviewCount = reviewCount;
        }

        /// <summary>
        /// Overload constructor sets <see cref="_reviewCount"/> to parameter and base readcount to readCount;
        /// </summary>
        /// <param name="reviewCount">How many reviews are initially</param>
        public Example2(int reviewCount, int readCount):base(readCount)
        {
            this._reviewCount = reviewCount;
        }
    }
}