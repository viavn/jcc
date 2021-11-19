export interface Child {
  id: string;
  name: string;
  age: string;
  clothesSize: string;
  shoeSize: string;
  legalResponsible: string;
  familyAcronym: string;
  familyPhone: string;
  familyAddress: string;
  godParents: GodParent[];
}

export interface DashChildModel {
  id: string;
  name: string;
  legalResponsible: string;
  familyAcronym: string;
}

export interface GodParent {
  id: string | undefined;
  name: string;
  phone: string;
  isClothesSelected: boolean;
  isShoeSelected: boolean;
  isGiftSelected: boolean;
}
