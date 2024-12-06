export interface Person {
    id?: number;
    names: string;
    firstSurname: string;
    lastSurname: string;
    gender: string;
    dateOfBirth: Date;
    identification: string;
    death?: {
      dateOfDeath?: Date;
      causeOfDeath?: string;
      locationOfDeath?: string;
      cityId?: number;
      villageId?: number;
    };
    cityId: number;
    villageId: number;
  }
  