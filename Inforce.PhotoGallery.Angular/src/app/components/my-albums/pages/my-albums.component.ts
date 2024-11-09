import { Component } from '@angular/core';
import { AlbumsApiService } from '../../../api/services/albums-api.service';
import { IAlbum } from '../../albums/models/albums.model';

@Component({
  selector: 'app-my-albums',
  templateUrl: './my-albums.component.html',
})
export class MyAlbumsComponent {

  albums: IAlbum[] = [];
  page = 1;
  pageSize = 5;
  totalPages = 1;

  isModalOpen = false;

  constructor(private albumService: AlbumsApiService) { }

  ngOnInit() {
    this.loadAlbums();
  }

  loadAlbums() {
    this.albumService.getUserAlbums(this.page, this.pageSize).subscribe({
      next: (response) => {
        this.albums = response.items;
        this.page = response.pageNumber;
        this.totalPages = response.totalPages;
        console.log(response);
      },
      error: (err) => {
        console.error('Failed to fetch albums', err);
      },
    });
  }

  onPageChange(newPage: number) {
    this.page = newPage;
    this.loadAlbums();
  }

  deleteAlbum(albumId: number, event: any) {
    event.stopPropagation();

    this.albumService.deleteAlbum(albumId).subscribe({
      next: () => {
        this.loadAlbums();
      },
      error: (err) => {
        console.error('Failed to fetch albums', err);
      }
    });

  }

  openModal() {
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
  }
}
