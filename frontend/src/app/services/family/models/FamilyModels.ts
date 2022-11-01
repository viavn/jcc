import { TypeResponse } from "../../child/models/Child";

export interface FamilyResponse {
  id: string;
  code: string;
  address: string;
  memberName: string;
}

export interface FamilyByIdResponse {
  id: string;
  code: string;
  contactNumber: string;
  address: string;
  comment: string;
  members: FamilyMemberResponse[];
  children: ChildResponse[];
}

export interface ChildResponse {
  id: string;
  name: string;
  age: string;
}

export interface FamilyMemberResponse {
  id: string;
  name: string;
  legalPerson: TypeResponse;
}

export interface BaseFamilyRequest {
  code: string;
  contactNumber: string;
  address: string;
  comment: string;
}

export interface CreateFamilyRequest extends BaseFamilyRequest {
  members: MemberRequest[];
}

export interface MemberRequest {
  id: string;
  name: string;
  type: number;
}

export interface MemberViewModel {
  rowId: number;
  member: MemberRequest;
}
