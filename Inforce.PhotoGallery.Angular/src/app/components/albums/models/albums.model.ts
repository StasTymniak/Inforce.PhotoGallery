export interface IAlbum {
  id: number;
  name: string;
  userId?: number;
  createdAt: Date;
  base64Url?: string;
}
