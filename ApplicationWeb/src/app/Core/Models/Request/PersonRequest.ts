import { DeathDetails } from "./DeathDetails";

export interface PersonRequest {
    names: string;
    description: string;
    firstSurname: string;
    lastSurname: string;
    cityId: number;
    gender: number;
    dateOfBirth: Date;
    identification: string;
    villageId: number;
    death: DeathDetails;
    isDeceased: boolean;
  }