export interface Child {
  id: string;
  name: string;
  age: string;
  clothesSize: string;
  shoeSize: string;
  familyId: string;
  responsible: string;
  phone: string;
  address: string;
}

export interface GodParent {
  id: string;
  name: string;
  phone: string;
  childId: string;
  isClothesSelected: boolean;
  isShoeSelected: boolean;
  isGiftSelected: boolean;
}
