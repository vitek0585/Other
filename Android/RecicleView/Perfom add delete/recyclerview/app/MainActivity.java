package recyclerview.sriyank.com.recyclerview.app;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.DefaultItemAnimator;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;

import recyclerview.sriyank.com.recyclerview.R;
import recyclerview.sriyank.com.recyclerview.adapter.RecyclerAdapter;
import recyclerview.sriyank.com.recyclerview.model.Landscape;

public class MainActivity extends AppCompatActivity {

	Toolbar toolbar;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);

		toolbar = (Toolbar) findViewById(R.id.toolbar);
		toolbar.setTitle("Operations on Items");
		toolbar.inflateMenu(R.menu.menu_main);

		setUpRecyclerView();
	}

	private void setUpRecyclerView() {

		RecyclerView recyclerView = (RecyclerView) findViewById(R.id.recyclerView);
		RecyclerAdapter adapter = new RecyclerAdapter(this, Landscape.getData());
		recyclerView.setAdapter(adapter);

		LinearLayoutManager mLinearLayoutManagerVertical = new LinearLayoutManager(this); // (Context context, int spanCount)
		mLinearLayoutManagerVertical.setOrientation(LinearLayoutManager.VERTICAL);
		recyclerView.setLayoutManager(mLinearLayoutManagerVertical);

		recyclerView.setItemAnimator(new DefaultItemAnimator()); // Even if we dont use it then also our items shows default animation. #Check Docs
	}
}
