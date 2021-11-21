export interface SystemNotification {
  Type: NotificationType;
  Message: string;
  ShowtimeInMilliseconds: number;
  ShowNotification: boolean;
}

export enum NotificationType {
  ERROR = 1,
  WARN = 2,
  INFO = 3,
}
