import React, { useState, useEffect } from 'react';
import { IconButton, Menu, MenuItem } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import { getTypes } from '../../api/type';
import { CatalogType } from '../../interfaces/items';

interface TypesMenuProps {
  onTypeSelect: (typeId: number) => void;
}

const TypesMenu: React.FC<TypesMenuProps> = ({ onTypeSelect }) => {
  const [types, setTypes] = useState<CatalogType[]>([]);
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

  useEffect(() => {
    const fetchTypes = async () => {
      try {
        const typesResponse = await getTypes();
        console.log("Отримані типи:", typesResponse);
        setTypes(typesResponse);
      } catch (error) {
        console.error('Помилка завантаження типів:', error);
      }
    };

    fetchTypes();
  }, []);

  const handleMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleMenuClose = (typeId?: number) => {
    setAnchorEl(null);
    if (typeId !== undefined) {
      if (typeId === 0) {
        window.location.reload(); // Оновлюємо поточну сторінку
      } else {
        onTypeSelect(typeId); // Виклик колбека для вибору конкретного типу
      }
    }
  };

  return (
    <div>
      <IconButton edge="start" color="inherit" aria-label="menu" onClick={handleMenuOpen}>
        <MenuIcon />
      </IconButton>
      <Menu anchorEl={anchorEl} open={Boolean(anchorEl)} onClose={() => handleMenuClose()}>
        <MenuItem onClick={() => handleMenuClose(0)}>All Types</MenuItem> {/* Передаємо 0 для вибору "All Types" */}
        {types.length > 0 ? (
          types.map((type) => (
            <MenuItem key={type.id} onClick={() => handleMenuClose(type.id)}>
              {type.type}
            </MenuItem>
          ))
        ) : (
          <MenuItem disabled>Немає типів</MenuItem>
        )}
      </Menu>
    </div>
  );
};

export default TypesMenu;