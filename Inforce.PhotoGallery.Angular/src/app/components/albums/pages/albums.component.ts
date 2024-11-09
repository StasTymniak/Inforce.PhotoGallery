import { Component } from '@angular/core';
import { IAlbum } from '../models/albums.model';
import { AlbumsApiService } from '../../../api/services/albums-api.service';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './albums.component.html',
})
export class AlbumsComponent {

  albums: IAlbum[] = [];
  page = 1;
  pageSize = 5;
  totalPages = 1;

  constructor(private albumService: AlbumsApiService, public authService: AuthService) { }

  ngOnInit() {
    this.loadAlbums();
  }

  loadAlbums() {
    this.albumService.getAlbums(this.page, this.pageSize).subscribe({
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
}
