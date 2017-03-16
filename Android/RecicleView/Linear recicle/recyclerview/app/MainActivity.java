package recyclerview.sriyank.com.recyclerview.app;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.DefaultItemAnimator;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;

import recyclerview.sriyank.com.recyclerview.R;
import recyclerview.sriyank.com.recyclerview.adapter.RecyclerAdapter;
import recyclerview.sriyank.com.recyclerview.controls.ToolBarExtend;
import recyclerview.sriyank.com.recyclerview.model.Landscape;

/** 
 * Author: Sriyank Siddhartha 
 * Module 4 : Understanding RecyclerViews and CardViews
 *
 *		"AFTER" demo project of : RecyclerView with CardView
 **/
public class MainActivity extends AppCompatActivity {

	private ToolBarExtend toolbar;
	RecyclerAdapter adapter;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);


		toolbar = (ToolBarExtend) findViewById(R.id.toolbar);


		setUpRecyclerView();
	}

	private void setUpRecyclerView() {

		RecyclerView recyclerView = (RecyclerView) findViewById(R.id.recyclerView);
		adapter = new RecyclerAdapter(this, Landscape.getData()){

		};
		recyclerView.setAdapter(adapter);

		LinearLayoutManager mLinearLayoutManagerVertical = new LinearLayoutManager(this); // (Context context, int spanCount)
		mLinearLayoutManagerVertical.setOrientation(LinearLayoutManager.VERTICAL);
		recyclerView.setLayoutManager(mLinearLayoutManagerVertical);

		recyclerView.setItemAnimator(new DefaultItemAnimator()); // Even if we dont use it then also our items shows default animation. #Check Docs
	}

}
