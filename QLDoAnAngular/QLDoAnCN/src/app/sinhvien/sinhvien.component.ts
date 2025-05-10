import { Component, OnInit } from '@angular/core';
import { SinhVienService } from '../services/sinhvien.service';
import { SinhVien } from '../models/sinhvien.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PopupComponent } from '../shared/popup/popup.component';
import { LopService } from '../services/lop.service';

@Component({
  selector: 'app-sinh-vien',
  standalone: true,
  imports: [CommonModule, FormsModule, PopupComponent],
  templateUrl: './sinhvien.component.html',
  styleUrl: './sinhvien.component.css'
})
export class SinhVienComponent implements OnInit {
  danhSachSinhVien: SinhVien[] = [];
  selectedSinhVien: SinhVien | null = null;
  isAddingNew: boolean = false;
  newSinhVien: SinhVien = { maSV: '', hoTen: '', email: '', soDienThoai: '', ngaySinh: null, maLop: '' };
  isEditing: boolean = false;
  editingSinhVien: SinhVien | null = null;
  errorMessage: string = '';
  successMessage: string = '';
  isViewingDetails: boolean = false;
  lops: { maLop: string, tenLop: string }[] = [];

  constructor(private sinhVienService: SinhVienService, private lopService: LopService) { }

  ngOnInit(): void {
    this.loadDanhSachSinhVien();
    this.loadDanhSachLop();
  }

  loadDanhSachSinhVien(): void {
    this.sinhVienService.getAllSinhVien().subscribe({
      next: (data) => {
        this.danhSachSinhVien = data;
      },
      error: (error) => {
        this.errorMessage = 'Lỗi khi tải danh sách sinh viên.';
        console.error('Lỗi tải danh sách sinh viên', error);
      }
    });
  }

  loadDanhSachLop(): void {
    this.lopService.getAllLop().subscribe({
      next: (data) => {
        this.lops = data;
      },
      error: (error) => {
        console.error('Lỗi khi tải danh sách lớp', error);
        this.errorMessage = 'Lỗi khi tải danh sách lớp.';
      }
    });
  }

  hienThiChiTiet(sinhVien: SinhVien): void {
    this.selectedSinhVien = sinhVien;
    this.isViewingDetails = true;
  }

  batDauThemMoi(): void {
    this.isAddingNew = true;
    this.newSinhVien = { maSV: '', hoTen: '', email: '', soDienThoai: '', ngaySinh: null, maLop: '' };
  }

  batDauChinhSua(sinhVien: SinhVien): void {
    this.editingSinhVien = { ...sinhVien };
    this.isEditing = true;
  }

  dongPopupChiTiet(): void {
    this.isViewingDetails = false;
    this.selectedSinhVien = null;
  }

  dongPopupThemMoi(): void {
    this.isAddingNew = false;
  }

  dongPopupChinhSua(): void {
    this.isEditing = false;
    this.editingSinhVien = null;
  }

  themSinhVien(): void {
    this.sinhVienService.addSinhVien(this.newSinhVien).subscribe({
      next: (response) => {
        this.successMessage = 'Thêm sinh viên thành công.';
        this.loadDanhSachSinhVien();
        this.isAddingNew = false;
        setTimeout(() => this.successMessage = '', 3000);
      },
      error: (error) => {
        this.errorMessage = 'Lỗi khi thêm sinh viên.';
        console.error('Lỗi thêm sinh viên', error);
      }
    });
  }

  capNhatSinhVien(): void {
    if (this.editingSinhVien) {
      this.sinhVienService.updateSinhVien(this.editingSinhVien.maSV, this.editingSinhVien).subscribe({
        next: (response) => {
          this.successMessage = 'Cập nhật sinh viên thành công.';
          this.loadDanhSachSinhVien();
          this.isEditing = false;
          this.editingSinhVien = null;
          setTimeout(() => this.successMessage = '', 3000);
        },
        error: (error) => {
          this.errorMessage = 'Lỗi khi cập nhật sinh viên.';
          console.error('Lỗi cập nhật sinh viên', error);
        }
      });
    }
  }

  huyThemMoi(): void {
    this.isAddingNew = false;
  }

  huyChinhSua(): void {
    this.isEditing = false;
    this.editingSinhVien = null;
  }

  xoaSinhVien(maSV: string): void {
    if (confirm('Bạn có chắc chắn muốn xóa sinh viên này?')) {
      this.sinhVienService.deleteSinhVien(maSV).subscribe({
        next: (response) => {
          this.successMessage = 'Xóa sinh viên thành công.';
          this.loadDanhSachSinhVien();
          this.selectedSinhVien = null;
          setTimeout(() => this.successMessage = '', 3000);
        },
        error: (error) => {
          this.errorMessage = 'Lỗi khi xóa sinh viên.';
          console.error('Lỗi xóa sinh viên', error);
        }
      });
    }
  }
}