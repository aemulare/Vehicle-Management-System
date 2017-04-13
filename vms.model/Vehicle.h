//=========================================================================================
// Vehicle domain model.
// A base abstract class for all kind of vehicles.
// Contains vehicle manufacturer properties.
//=========================================================================================
#pragma once
#include "Stdafx.h"
#include "PersistentEntity.h"

namespace vms
{
   public ref class Vehicle abstract : public PersistentEntity
   {
   public:
      // Available vehicle fuel types
      enum class FuelType { GASOLINE, DIESEL, BIODIESEL, ELECTRICITY, ETHANOL, HYBRID, LIQUEFIED_PETROLEUM };
      // Available vehicle transmission types
      enum class Transmission { AUTOMATIC, SEMI_AUTOMATIC, MANUAL, ELECTRIC_VARIABLE, CONTINUOUSLY_VARIABLE };

   protected:
      Vehicle();
      Vehicle(Vehicle^ vehicle);

   public:
      string getVIN();
      string getMaker();
      string getModel();
      FuelType getFuelType();
      Transmission getTransmission();
      int getYearManufactured();
      double getSweptVolume();
      double getFuelTankCapacity();
      string getColor();

      void setVIN(const string vin);
      void setMaker(const string maker);
      void setModel(const string model);
      void setFuelType(FuelType fuel);
      void setTransmission(Transmission transmission);
      void setYearManufactured(int year);
      void setSweptVolume(double sweptVolume);
      void setFuelTankCapacity(double tankCapacity);
      void setColor(const string color);

      string toString();

   private:
      string _vin;                        // vehicle identification number
      string _maker;
      string _model;
      FuelType _fuelType;
      Transmission _transmission;
      int _yearManufactured;
      double _sweptVolume;                // car engine size
      double _fuelTankCapacity;           // fuel tank size
      string _color;
   };
}
