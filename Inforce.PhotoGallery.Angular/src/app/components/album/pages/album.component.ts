import { Component } from '@angular/core';
import { ImagesApiService } from '../../../api/services/images-api.service';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { AlbumsApiService } from '../../../api/services/albums-api.service';
import { IImage } from '../models/image';

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html',
})
export class AlbumComponent {

  images: IImage[] = [];
  albumOwnerId: number | undefined;
  selectedImage: string | null = null;
  page = 1;
  pageSize = 5;
  totalPages = 1;
  albumId = 0;

  constructor(private albumService: AlbumsApiService, private imageService: ImagesApiService, private activatedRoute: ActivatedRoute, public authService: AuthService) { }

  openImageModal(imageUrl: string) {
    this.selectedImage = 'https://localhost:44387/Images/' + imageUrl;
  }

  closeImageModal() {
    this.selectedImage = null;
  }

  ngOnInit() {
    this.albumId = +this.activatedRoute.snapshot.paramMap.get('id')!;
    this.getAlbumOwnerId();
    this.loadImages();
  }

  loadImages() {
    this.imageService.getImages(this.page, this.albumId, this.pageSize).subscribe({
      next: (response) => {
        this.images = response.items;
        this.page = response.pageNumber;
        this.totalPages = response.totalPages;
        console.log(this.images);
      },
      error: (err) => {
        console.error('Failed to fetch images', err);
      },
    });
  }

  onPageChange(newPage: number) {
    this.page = newPage;
    this.loadImages();
  }

  getAlbumOwnerId() {
    this.albumService.getAlbumById(this.albumId).subscribe({
      next: (responce) => {
        this.albumOwnerId = responce.userId;
        console.log(this.albumOwnerId);
      },
      error: (err) => {
        console.error('Failed to fetch album', err);
      },
    });

  }

  likeImage(image: IImage) {
    if (image.currentUserLiked === null || image.currentUserLiked === false) {
      image.countLike += 1;
      if (image.currentUserLiked === false) {
        image.countDislike -= 1;
      }
      image.currentUserLiked = true;
      this.imageService.reactionImage(image.id, true).subscribe({
        next: () => {
          this.loadImages();
        },
        error: (err) => {
          console.error('Failed to fetch albums', err);
        }
      });
    }
  }

  dislikeImage(image: IImage) {
    if (image.currentUserLiked === null || image.currentUserLiked === true) {
      image.countDislike += 1;
      if (image.currentUserLiked === true) {
        image.countLike -= 1;
      }
      image.currentUserLiked = false;
      this.imageService.reactionImage(image.id, false).subscribe({
        next: () => {
          this.loadImages();
        },
        error: (err) => {
          console.error('Failed to fetch albums', err);
        }
      });
    }
  }

  deleteImage(imageId: number, event: any) {
    event.stopPropagation();

    this.imageService.deleteImage(imageId).subscribe({
      next: () => {
        this.loadImages();
      },
      error: (err) => {
        console.error('Failed to fetch images', err);
      }
    });
  }
}
