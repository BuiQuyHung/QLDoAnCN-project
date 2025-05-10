import { Component, OnInit } from '@angular/core';
import { KhoaService } from '../services/khoa.service';
import { Khoa } from '../models/khoa.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PopupComponent } from '../shared/popup/popup.component';

@Component({
  selector: 'app-khoa',
  standalone: true,
  imports: [CommonModule, FormsModule, PopupComponent],
  templateUrl: './khoa.component.html',
  styleUrl: './khoa.component.css'
})
export class KhoaComponent implements OnInit {
  danhSachKhoa: Khoa[] = [];
  selectedKhoa: Khoa | null = null;
  isAddingNew: boolean = false;
  newKhoa: Khoa = { maKhoa: '', tenKhoa: '' };
  isEditing: boolean = false;
  editingKhoa: Khoa | null = null;
  errorMessage: string = '';
  successMessage: string = '';
  isViewingDetails: boolean = false;
 
  constructor(private khoaService: KhoaService) { }

  ngOnInit(): void {
    this.loadDanhSachKhoa();
  }

  loadDanhSachKhoa(): void {
    this.khoaService.getAllKhoa().subscribe({
      next: (data) => {
        this.danhSachKhoa = data;
      },
      error: (error) => {
        this.errorMessage = 'Lỗi khi tải danh sách khoa.';
        console.error('Lỗi tải danh sách khoa', error);
      }
    });
  }

  hienThiChiTiet(khoa: Khoa): void {
    this.selectedKhoa = khoa;
    this.isViewingDetails = true;
  }
  
  batDauThemMoi(): void {
    this.isAddingNew = true;
    this.newKhoa = { maKhoa: '', tenKhoa: '' };
  }
  
  batDauChinhSua(khoa: Khoa): void {
    this.editingKhoa = { ...khoa };
    this.isEditing = true;
  }
  
  dongPopupChiTiet(): void {
    this.isViewingDetails = false;
    this.selectedKhoa = null;
  }
  
  dongPopupThemMoi(): void {
    this.isAddingNew = false;
  }
  
  dongPopupChinhSua(): void {
    this.isEditing = false;
    this.editingKhoa = null;
  }
  
  themKhoa(): void {
    this.khoaService.addKhoa(this.newKhoa).subscribe({
      next: (response) => {
        this.successMessage = 'Thêm khoa thành công.';
        this.loadDanhSachKhoa();
        this.isAddingNew = false; 
        setTimeout(() => this.successMessage = '', 3000);
      },
      error: (error) => {
        this.errorMessage = 'Lỗi khi thêm khoa.';
        console.error('Lỗi thêm khoa', error);
      }
    });
  }
  
  capNhatKhoa(): void {
    if (this.editingKhoa) {
      this.khoaService.updateKhoa(this.editingKhoa.maKhoa, this.editingKhoa).subscribe({
        next: (response) => {
          this.successMessage = 'Cập nhật khoa thành công.';
          this.loadDanhSachKhoa();
          this.isEditing = false; 
          this.editingKhoa = null;
          setTimeout(() => this.successMessage = '', 3000);
        },
        error: (error) => {
          this.errorMessage = 'Lỗi khi cập nhật khoa.';
          console.error('Lỗi cập nhật khoa', error);
        }
      });
    }
  }
  
  huyThemMoi(): void {
    this.isAddingNew = false; 
  }
  
  huyChinhSua(): void {
    this.isEditing = false; 
    this.editingKhoa = null;
  }
  // capNhatKhoa(): void {
  //   if (this.editingKhoa) {
  //     this.khoaService.updateKhoa(this.editingKhoa.maKhoa, this.editingKhoa).subscribe({
  //       next: (response) => {
  //         this.successMessage = 'Cập nhật khoa thành công.';
  //         this.loadDanhSachKhoa();
  //         this.isEditing = false;
  //         this.editingKhoa = null;
  //         setTimeout(() => this.successMessage = '', 3000);
  //       },
  //       error: (error) => {
  //         this.errorMessage = 'Lỗi khi cập nhật khoa.';
  //         console.error('Lỗi cập nhật khoa', error);
  //       }
  //     });
  //   }
  // }

  xoaKhoa(maKhoa: string): void {
    if (confirm('Bạn có chắc chắn muốn xóa khoa này?')) {
      this.khoaService.deleteKhoa(maKhoa).subscribe({
        next: (response) => {
          this.successMessage = 'Xóa khoa thành công.';
          this.loadDanhSachKhoa();
          this.selectedKhoa = null;
          setTimeout(() => this.successMessage = '', 3000);
        },
        error: (error) => { 
          this.errorMessage = 'Lỗi khi xóa khoa.';
          console.error('Lỗi xóa khoa', error); 
        }
      });
    }
  }
}