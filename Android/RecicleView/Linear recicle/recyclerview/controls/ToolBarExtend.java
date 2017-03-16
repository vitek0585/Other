package recyclerview.sriyank.com.recyclerview.controls;

import android.content.Context;
import android.support.v7.widget.Toolbar;
import android.util.AttributeSet;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.MenuItem;
import android.view.View;

import recyclerview.sriyank.com.recyclerview.R;


public class ToolBarExtend extends Toolbar {
    public ToolBarExtend(Context context, AttributeSet attributeSet) {
        super(context,attributeSet);

//        LayoutInflater inflater = (LayoutInflater)
//                context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
//
//        inflater.inflate(R.layout.toolbar, this);

        inflateMenu(R.menu.menu_main);
        setTitle("Home Page");
        setOnMenuItemClickListener(new Toolbar.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                Log.i("MENU", String.valueOf(item.getItemId()));

                return true;
            }
        });
    }



}
