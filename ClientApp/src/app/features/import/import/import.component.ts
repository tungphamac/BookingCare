import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-import',
  templateUrl: './import.component.html',
  styleUrls: ['./import.component.css']
})
export class ImportComponent {
  constructor(private http: HttpClient) {}

  onFileChange(event: any): void {
    const file = event.target.files[0];

    if (file) {
      const formData = new FormData();
      formData.append('file', file);

      // Gửi file lên backend
      this.http.post('https://localhost:7182/api/Specialization/import', formData).subscribe({
        next: (res) => {
          alert('Import thành công!');
        },
        error: (err) => {
          console.error('Lỗi khi import:', err);
          alert('Import thất bại!');
        }
      });
    }
  }
}
