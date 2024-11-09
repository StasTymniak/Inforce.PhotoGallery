import { Component, EventEmitter, Output } from '@angular/core';
import { AlbumsApiService } from '../../../api/services/albums-api.service';

@Component({
  selector: 'app-create-modal',
  templateUrl: './create-modal.component.html',
})
export class CreateModalComponent {
  @Output() close = new EventEmitter();
  @Output() fetchData = new EventEmitter();

  albumName = '';

  constructor(private albumsApiService: AlbumsApiService) { }

  submitModal() {
    this.albumsApiService.createAlbum(this.albumName)
      .subscribe({
        next: () => {
          this.fetchData.emit();
          this.closeModal();
        }
      })
  }

  closeModal() {
    this.close.emit();
  }
}
