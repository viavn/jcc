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

export interface GodParent {
  id: string | undefined;
  name: string;
  phone: string;
  isClothesSelected: boolean;
  isShoeSelected: boolean;
  isGiftSelected: boolean;
}

export interface DashChildModel {
  id: string;
  name: string;
  familyCode: string;
  deliveredGifts: number;
  remainingGifts: number;
}

export interface TypeResponse {
  id: number;
  description: string;
}

export interface GodParentResponse {
  id: string;
  name: string;
  contactNumber: string;
  address: string;
}

export interface FamilyChildResponse {
  id: string;
  code: string;
  contactNumber: string;
  address: string;
  member: string;
}

export interface GiftResponse {
  isDelivered: boolean;
  giftType: TypeResponse;
  godParent: GodParentResponse;
}

export interface GetChildrenByIdResponse {
  id: string;
  name: string;
  age: string;
  clotheSize: string;
  shoeSize: string;
  family: FamilyChildResponse;
  genre: TypeResponse;
  gifts: GiftResponse[];
}

export interface CreateGiftRequest {
  childId: string;
  typeId: number;
  godParent: Partial<GodParentResponse>;
}

export interface CreateChildRequest {
  id: string;
  familyId: string;
  name: string;
  age: string;
  clotheSize: string;
  shoeSize: string;
  genre: number;
}
