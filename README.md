# GameOfLife
Conway's Game Of Life implementation in C# by Ivor Chalton

See https://www.theguardian.com/science/alexs-adventures-in-numberland/2014/dec/15/the-game-of-life-a-beginners-guide
---

**Due to time constraints, the following are limitations that I'm well aware of that I'd normally tackle IRL:**

* Although I've utilized some basic IOC (ICellAcquirer, IOmnipotentBeing etc.), I've not used an IOC container. My go-to here is usually AutoFac
* Although I've created a Unit-testing project, the tests therein are not exhaustive
* Exception handling is rather minimal
* Huge (>Int32.MAX.sqrt()) world-sizes will not work due to the Cell-indexing method I chose -- which was for speed.
