export interface IImage {
  id: number;
  name: string;
  base64Url: string;
  albumId: number;
  userId: number;
  countLike: number;
  countDislike: number;
  currentUserLiked: boolean | null;
}
